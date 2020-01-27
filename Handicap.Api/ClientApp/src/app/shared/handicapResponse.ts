import { HandicapError } from './HandicapError';
export interface HandicapResponse {
    totalCount: number;
    cursor: number;
    pageSize: number;
    hasNext: boolean;
    hasPrevious: boolean;
    payload: any[];
    error: HandicapError;
}
