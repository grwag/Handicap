export class PlayerRequest {
    firstName: string;
    lastName: string;
    handicap: number;

    constructor(firstName: string, lastName: string, handicap: number) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.handicap = handicap;
    }
}
