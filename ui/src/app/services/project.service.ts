import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Project } from '../models/project';
import { map, catchError } from 'rxjs/operators';
import { BaseService } from './base.service';

@Injectable()
export class ProjectService extends BaseService {
    constructor(private http: HttpClient) {
        super();
    }

    getProject(): Observable<Project[]> {
        return this.http.get(super.baseurl() + 'project/getproj')
            .pipe(map((res: Response) => {
                const data = res["data"];
                return data;
            }))
            .pipe(catchError(this.handleError));
    }
    addProject(project:Project): Observable<any> {
        return this.http.post(super.baseurl() + 'project/addproj',project)
            .pipe(map((res: Response) => {
                const data = res["data"];
                return data;
            }))
            .pipe(catchError(this.handleError));
    }

    updateProject(project:Project): Observable<any> {
        return this.http.post(super.baseurl() + 'project/updateproj',project)
            .pipe(map((res: Response) => {
                const data = res["data"];
                return data;
            }))
            .pipe(catchError(this.handleError));
    }

    deleteProject(project:Project): Observable<any> {
        return this.http.post(super.baseurl() + 'project/deleteproj',project)
            .pipe(map((res: Response) => {
                const data = res["data"];
                return data;
            }))
            .pipe(catchError(this.handleError));
    }
}   