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
} from "../api/wrapped";
import { useAuth } from "./Auth";

export enum DataStatus {
  NOT_LOADED,
  LOADING,
  LOADED,
  ERROR,
}

interface APIContextType {
  taggs?: TaggPreviewModel[];
  taggsStatus: DataStatus;
  createTagg: typeof createTaggFromApi;
  createTaggInstance: typeof createTaggInstanceFromApi;
  getAllTaggs: typeof getAllTaggsFromApi;
}

const APIContext = createContext<APIContextType>({} as APIContextType);

/**
 * Creates a provider to use for running API calls and caching results
 * @param props The props for this component
 */
export const APIProvider: FunctionComponent<PropsWithChildren> = ({
  children,
}) => {
  const [taggs, setTaggs] = useState<TaggPreviewModel[]>([]);
  const [taggsStatus, setTaggsStatus] = useState(DataStatus.NOT_LOADED);
  const { user } = useAuth();

  /**
   * Creates a tagg and stores it locally for reference
   * @param name The name of the tagg to create
   * @returns The created tagg
   */
  const createTagg: APIContextType["createTagg"] = async (...args) => {
    const tagg = await createTaggFromApi(...args);
    setTaggs([...taggs, tagg]);
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
      setTaggsStatus(DataStatus.LOADING);
      const taggs = await getAllTaggsFromApi();
      setTaggs([...taggs]);
      setTaggsStatus(DataStatus.LOADED);
      return taggs;
    } catch (e) {
      setTaggsStatus(DataStatus.ERROR);
      throw e;
    }
  };

  useEffect(() => {
    if (user) {
      getAllTaggs();
    } else {
      setTaggs([]);
      setTaggsStatus(DataStatus.NOT_LOADED);
      // TODO: Remove when auth is complete
      getAllTaggs();
    }
  }, [user]);

  // Use Memo'd versions to prevent re-rendering unnecessarily
  const memoedValues = useMemo<APIContextType>(
    () => ({
      taggs,
      taggsStatus,
      createTagg,
      getAllTaggs,
      createTaggInstance,
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
