import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { TaggModel, TaggPreviewModel } from "../../api/generated";
import { AppThunk } from "../helper";

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

export interface APIState {
  taggs: DataWrapper<TaggPreviewModel[]>;
  taggDetails: { [key: string]: DataWrapper<TaggModel> };
}

// Initial state of the api store
const initialState: APIState = {
  taggs: {
    status: DataStatus.NOT_LOADED,
  },
  taggDetails: {},
};

export const apiSlice = createSlice({
  name: "API",
  initialState,
  reducers: {
    /**
     * Updates the taggs stored for the user
     * @param state The state of the app
     * @param action The action to take on the state
     */
    updateTaggs(state, action: PayloadAction<APIState["taggs"]>) {
      state.taggs = action.payload;
    },
    /**
     * Updates the tagg details stored in the state
     * @param state The state of the app
     * @param action The action to take on the state
     */
    updateAllTaggDetails(
      state,
      action: PayloadAction<APIState["taggDetails"]>
    ) {
      state.taggDetails = action.payload;
    },
    /**
     * Updates the details about a specific tagg
     * @param state The state of the app
     * @param action The action to take on the state
     */
    updateTaggDetails(
      state,
      action: PayloadAction<{ id: string; value: DataWrapper<TaggModel> }>
    ) {
      state.taggDetails = {
        ...state.taggDetails,
        [action.payload.id]: action.payload.value,
      };
    },
  },
});

/**
 * Updates the list of taggs stored in the store
 * @param status The status of the data
 * @param value The list of taggs to store
 * @param error Any errors
 * @returns A thunk function
 */
export function updateTaggs(
  status: DataStatus,
  value?: TaggPreviewModel[],
  error?: string
) {
  const updateTaggsThunk: AppThunk = (dispatch, _getState) => {
    dispatch(
      apiSlice.actions.updateTaggs({
        status,
        value,
        error,
      })
    );
  };
  return updateTaggsThunk;
}

/**
 * Adds a tagg to the list in the store
 * @param tagg The tagg to add
 * @returns A thunk function
 */
export function addTagg(tagg: TaggPreviewModel) {
  const addTaggThunk: AppThunk = (dispatch, getState) => {
    const taggs = getState().api.taggs;
    dispatch(
      apiSlice.actions.updateTaggs({
        ...taggs,
        value: taggs.value ? [...taggs.value, tagg] : [tagg],
      })
    );
  };
  return addTaggThunk;
}

/**
 * Updates the details about a tagg
 * @param taggId The id of the tagg
 * @param status The status of the tagg data
 * @param value The Tagg
 * @param error Any errors
 * @returns A thunk function
 */
export function updateTaggDetails(
  taggId: string,
  status: DataStatus,
  value?: TaggModel,
  error?: string
) {
  const updateTaggDetailsThunk: AppThunk = (dispatch, _getState) => {
    dispatch(
      apiSlice.actions.updateTaggDetails({
        id: taggId,
        value: {
          status,
          value,
          error,
        },
      })
    );
  };
  return updateTaggDetailsThunk;
}

/**
 * Makes sure a tagg with given ID has an entry in the details object
 * @param taggId The id of the tagg
 * @returns A thunk function
 */
export function initialiseTaggDetails(taggId: string) {
  /**
   * @returns true if the tagg has already been initialised, false if not
   */
  const initialiseTaggDetailsThunk: AppThunk<boolean> = (
    dispatch,
    getState
  ) => {
    const tagg = getState().api.taggDetails[taggId];
    if (!tagg) {
      dispatch(updateTaggDetails(taggId, DataStatus.NOT_LOADED));
      return false;
    }
    return tagg.status !== DataStatus.NOT_LOADED;
  };
  return initialiseTaggDetailsThunk;
}

export default apiSlice.reducer;
