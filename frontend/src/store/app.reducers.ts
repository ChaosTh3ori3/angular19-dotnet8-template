import { createReducer, on } from "@ngrx/store";
import { setPageTitle } from "./app.actions";

export interface AppState{
    pageTitle: string | undefined;
}

const INITIAL_APP_STATE: AppState = {
    pageTitle: undefined
}

export const appReducer = createReducer(
    INITIAL_APP_STATE,
    on(setPageTitle, (state, action) => ({
        ...state,
        pageTitle: action.pageTitle
    }))
);

export const appReducers = {
    app: appReducer
}