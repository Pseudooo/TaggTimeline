import {
  createContext,
  FunctionComponent,
  PropsWithChildren,
  useContext,
  useEffect,
  useMemo,
} from "react";
import { TaggModel, TaggPreviewModel } from "../api/generated";
import {
  createTagg as createTaggFromApi,
  getAllTaggs as getAllTaggsFromApi,
  createTaggInstance as createTaggInstanceFromApi,
  getTagg as getTaggFromApi,
} from "../api/wrapped";
import { useAppDispatch, useAppSelector } from "../store/helper";
import {
  addTagg,
  addTaggInstance,
  initialiseTaggDetails,
  updateTaggDetails,
  updateTaggs,
} from "../store/reducers/api";
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
  useTaggs: () => DataWrapper<TaggPreviewModel[]>;
  useTaggDetails: () => { [key: string]: DataWrapper<TaggModel> };
  createTagg: typeof createTaggFromApi;
  createTaggInstance: typeof createTaggInstanceFromApi;
  getAllTaggs: typeof getAllTaggsFromApi;
  initTaggDetails: (taggId: string) => void;
  getTaggDetails: typeof getTaggFromApi;
}

const APIContext = createContext<APIContextType>({} as APIContextType);

/**
 * Creates a provider to use for running API calls and caching results
 * @param props The props for this component
 */
export const APIProvider: FunctionComponent<PropsWithChildren> = ({
  children,
}) => {
  const dispatch = useAppDispatch();
  const { user } = useAuth();

  const useTaggs = () => useAppSelector((state) => state.api.taggs);
  const useTaggDetails = () => useAppSelector((state) => state.api.taggDetails);

  /**
   * Creates a tagg and stores it locally for reference
   * @param name The name of the tagg to create
   * @returns The created tagg
   */
  const createTagg: APIContextType["createTagg"] = async (...args) => {
    const tagg = await createTaggFromApi(...args);
    dispatch(addTagg(tagg));
    return tagg;
  };

  /**
   * Creates an instance of a tag
   * @param taggId The id of the tag
   * @returns The created instance
   */
  const createTaggInstance: APIContextType["createTaggInstance"] = async (
    id,
    ...args
  ) => {
    const instance = await createTaggInstanceFromApi(id, ...args);
    dispatch(addTaggInstance(id, instance));
    return instance;
  };

  /**
   * Gets all the user's taggs
   * @returns All available taggs
   */
  const getAllTaggs: APIContextType["getAllTaggs"] = async () => {
    try {
      dispatch(updateTaggs(DataStatus.LOADING));
      const taggs = await getAllTaggsFromApi();
      dispatch(updateTaggs(DataStatus.LOADED, taggs));
      return taggs;
    } catch (e) {
      dispatch(updateTaggs(DataStatus.ERROR, undefined, `${e}`));
      throw e;
    }
  };

  /**
   * Gets a tagg by its ID
   * @param params Parameters from the getTagg API function
   * @returns The tagg, if found
   */
  const getTaggDetails: APIContextType["getTaggDetails"] = async (id) => {
    try {
      dispatch(updateTaggDetails(id, DataStatus.LOADING));
      const tagg = await getTaggFromApi(id);
      dispatch(updateTaggDetails(id, DataStatus.LOADED, tagg));
      return tagg;
    } catch (e) {
      dispatch(updateTaggDetails(id, DataStatus.ERROR, undefined, `${e}`));
      throw e;
    }
  };

  /**
   * Loads a tagg's details if it hasn't been loaded
   * @param taggId The id of the tagg
   */
  const initTaggDetails = (taggId: string) => {
    const initialised = dispatch(initialiseTaggDetails(taggId));
    if (!initialised) {
      getTaggDetails(taggId);
    }
  };

  useEffect(() => {
    if (user) {
      getAllTaggs();
    } else {
      dispatch(updateTaggs(DataStatus.NOT_LOADED));
      // TODO: Remove when auth is complete
      getAllTaggs();
    }
  }, [user]);

  // Use Memo'd versions to prevent re-rendering unnecessarily
  const memoedValues = useMemo<APIContextType>(
    () => ({
      useTaggs,
      useTaggDetails,
      createTagg,
      getAllTaggs,
      createTaggInstance,
      getTaggDetails,
      initTaggDetails,
    }),
    []
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
