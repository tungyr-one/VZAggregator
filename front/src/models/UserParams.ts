export class UserParams{
    offset:number;
    pageSize:number;
    sortBy:string;
    sortOrder: string;
    filterBy: string;

    constructor() {
        this.filterBy = '';
        this.sortOrder = "asc";
        this.sortBy = 'date';
        this.offset = 0;
        this.pageSize = 5;
      }

    // constructor(offset:number, pageSize:number, sortBy:string,sortOrder:string, filterBy:string)
    // {
    //     this.offset = offset;
    //     this.pageSize = pageSize;
    //     this.sortBy = sortBy;
    //     this.sortOrder = sortOrder;
    //     this.filterBy = filterBy;
    // }
 }