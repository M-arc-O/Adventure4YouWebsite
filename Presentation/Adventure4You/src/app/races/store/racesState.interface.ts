import { createFeatureSelector, createSelector, MemoizedSelector } from '@ngrx/store';
import { IRaces } from './races.interface';
import { ISelectedRace } from './selectedRace.interface';

export interface IRacesState {
    races: IRaces;
    selectedRace: ISelectedRace;
}

export const racesFeatureSelector: MemoizedSelector<object, IRacesState> = createFeatureSelector<IRacesState>('racesFeature');

export const racesStateSelector: MemoizedSelector<object, IRaces> = createSelector(racesFeatureSelector, s => s.races);
export const selectedRaceStateSelector: MemoizedSelector<object, ISelectedRace> = createSelector(racesFeatureSelector, s => s.selectedRace);
