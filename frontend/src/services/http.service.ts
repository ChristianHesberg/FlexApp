import { Injectable } from '@angular/core';
import axios from 'axios';
import {MatSnackBar} from "@angular/material/snack-bar";
import {catchError, Observable} from "rxjs";
import {Session} from "../app/session";

export const customAxios = axios.create({
  baseURL: 'https://localhost:5001'
})

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private matSnackBar: MatSnackBar) {
    customAxios.interceptors.response.use(
      response => {
        if(response.status==201){
          this.matSnackBar.open("Very success Wow")
        }
        return response;
      }, rejected =>{
        if(rejected.response.status>=400 && rejected.response < 500) {
          this.matSnackBar.open(rejected.response.data);
        }
        else if(rejected.response.status > 499){
          this.matSnackBar.open("Something went wrong");
        }
        catchError(rejected);
      }
    )
  }

  async getSessions(): Promise<Session[]>{
    const response = await customAxios.get<Session[]>('session');
    return response.data;
  }

  async insertSession(dto: { startTime: Date; employeeId: number; endTime: Date }) {
    const result = await customAxios.post('session', dto);
    return result.data;
  }

  async deleteSession(id: number) {
    const result = await customAxios.delete('session/'+id)
    return result.data;
  }
}
