import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { map,catchError } from 'rxjs/operators';
import { BaseService } from './base.service';

@Injectable()
export class UserService extends BaseService {
    constructor(private http: HttpClient) {
        super();
    }

    getUser(): Observable<User[]> {
        return this.http.get(super.baseurl() + 'user/users')
            .pipe(map((res: Response) => {
                const data = res["data"];
                return data;
            }))
            .pipe(catchError(this.handleError));
    }
    
    addUser(user:User): Observable<any> {
        return this.http.post(super.baseurl() + 'user/adduser',user)
            .pipe(map((res: Response) => {
                const data = res["data"];
                return data;
            }))
            .pipe(catchError(this.handleError));
    }

    updateUser(user:User): Observable<any> {
        return this.http.post(super.baseurl() + 'user/updateuser',user)
            .pipe(map((res: Response) => {
                const data = res["data"];
                return data;
            }))
            .pipe(catchError(this.handleError));
    }

   deleteUser(user:User): Observable<any> {
        return this.http.post(super.baseurl() + 'user/deleteuser',user)
            .pipe(map((res: Response) => {
                const data = res["data"];
                return data;
            }))
            .pipe(catchError(this.handleError));
    }
}   