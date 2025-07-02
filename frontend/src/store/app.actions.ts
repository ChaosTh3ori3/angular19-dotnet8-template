import { createAction, props } from "@ngrx/store";

export const setPageTitle = createAction(
    '[App] Set page title',
    props<{ pageTitle: string | undefined }>() 
);