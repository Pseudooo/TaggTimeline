import {
  createContext,
  FunctionComponent,
  PropsWithChildren,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { TaggPreviewModel } from "../api/generated";
import {
  createTagg as createTaggFromApi,
  getAllTaggs as getAllTaggsFromApi,
  createTaggInstance as createTaggInstanceFromApi,
  getTagg as getTaggFromApi,
} from "../api/wrapped";
import { useAuth } from "./Auth";

export enum DataStatus {
  NOT_LOADED,
  LOADING,
  LOADED,
  ERROR,
}

export interface DataWrapper<T> {
  value?: T;
  status: DataStatus;
  error?: string;
}

interface APIContextType {
  taggs: DataWrapper<TaggPreviewModel[]>;
  createTagg: typeof createTaggFromApi;
  createTaggInstance: typeof createTaggInstanceFromApi;
  getAllTaggs: typeof getAllTaggsFromApi;
  getTagg: typeof getTaggFromApi;
}

const APIContext = createContext<APIContextType>({} as APIContextType);

/**
 * Creates a provider to use for running API calls and caching results
 * @param props The props for this component
 */
export const APIProvider: FunctionComponent<PropsWithChildren> = ({
  children,
}) => {
  const [taggs, setTaggs] = useState<DataWrapper<TaggPreviewModel[]>>({
    status: DataStatus.NOT_LOADED,
  });
  const { user } = useAuth();

  /**
   * Creates a tagg and stores it locally for reference
   * @param name The name of the tagg to create
   * @returns The created tagg
   */
  const createTagg: APIContextType["createTagg"] = async (...args) => {
    const tagg = await createTaggFromApi(...args);
    setTaggs((prev) => ({
      ...prev,
      value: prev.value ? [...prev.value, tagg] : [tagg],
    }));
    return tagg;
  };

  /**
   * Creates an instance of a tag
   * @param taggId The id of the tag
   * @returns The created instance
   */
  const createTaggInstance: APIContextType["createTaggInstance"] = async (
    ...args
  ) => {
    const instance = await createTaggInstanceFromApi(...args);
    return instance;
  };

  /**
   * Gets all the user's taggs
   * @returns All available taggs
   */
  const getAllTaggs: APIContextType["getAllTaggs"] = async () => {
    try {
      setTaggs((prev) => ({ ...prev, status: DataStatus.LOADING }));
      const taggs = await getAllTaggsFromApi();
      setTaggs((prev) => ({
        ...prev,
        status: DataStatus.LOADED,
        value: taggs,
      }));
      return taggs;
    } catch (e) {
      setTaggs((prev) => ({
        ...prev,
        status: DataStatus.ERROR,
        error: `${e}`,
      }));
      throw e;
    }
  };

  /**
   * Gets a tagg by its ID
   * @param params Parameters from the getTagg API function
   * @returns The tagg, if found
   */
  const getTagg: APIContextType["getTagg"] = async (...params) => {
    const tagg = await getTaggFromApi(...params);
    return tagg;
  };

  useEffect(() => {
    if (user) {
      getAllTaggs();
    } else {
      setTaggs({ status: DataStatus.NOT_LOADED });
      // TODO: Remove when auth is complete
      getAllTaggs();
    }
  }, [user]);

  // Use Memo'd versions to prevent re-rendering unnecessarily
  const memoedValues = useMemo<APIContextType>(
    () => ({
      taggs,
      createTagg,
      getAllTaggs,
      createTaggInstance,
      getTagg,
    }),
    [taggs]
  );

  return (
    <APIContext.Provider value={memoedValues}>{children}</APIContext.Provider>
  );
};

/**
 * Helper function for getting the app's API context
 */
export function useAPI() {
  return useContext(APIContext);
}
