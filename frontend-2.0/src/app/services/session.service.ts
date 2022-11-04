import { Injectable } from '@angular/core';
import axios from "axios";
import {Session} from "../Entities/session";
import {SessionDTO} from "../Entities/sessionDTO";
import {MatSnackBar} from "@angular/material/snack-bar";
import {catchError} from "rxjs";

export const customAxios = axios.create({
  baseURL: 'https://localhost:5001'
})

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  constructor(private matSnackBar: MatSnackBar) {
    customAxios.interceptors.response.use(
      response => {
        if(response.status==201){
          this.matSnackBar.open("Very success Wow")
        }
        return response;
      },
        rejected =>{
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

  async getSessionsForEmployee(employeeId: number): Promise<Session[]>{
    const response = await customAxios.get('session/user/'+employeeId);
    return response.data;
  }
  async createSession(dto: SessionDTO): Promise<Session>{
    const result = await customAxios.post('session', dto);
    return result.data;
  }

  async deleteSession(employeeId: number): Promise<Session>{
    const response = await customAxios.delete('session/'+employeeId);
    return response.data;
  }

  async editSession(session: Session): Promise<Session> {
    const response = await customAxios.put('session', session)
    if(response.status==200)
      this.matSnackBar.open("Successful edit")
    return response.data
  }
}
