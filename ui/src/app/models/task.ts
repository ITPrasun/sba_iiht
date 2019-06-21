import { User } from "./user";

export class Task {
    public StartDate: string;
    public EndDate: string;
    public TaskName: string;
    public ProjectID: number;
    public TaskId: number;
    public priority: number;
    public ParentID: number;
    public Status: number;
    public user: User;
    public parentTaskName: string;

    constructor() {
        this.user = new User();
    }
}
