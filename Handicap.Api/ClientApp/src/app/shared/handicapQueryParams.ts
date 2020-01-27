class HandicapQueryParams {
    orderBy: string;
    desc: boolean;
    pageSize: number;
    page: number;

    constructor(orderBy: string = '', desc: boolean = false, pageSize: number = 10, page: number = 0) {
        this.orderBy = orderBy;
        this.desc = desc;
        this.pageSize = pageSize;
        this.page = page;
    }
}
