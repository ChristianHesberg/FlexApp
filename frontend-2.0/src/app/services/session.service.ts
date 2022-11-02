import { Injectable } from '@angular/core';
import axios from "axios";
import {Session} from "../Entities/session";
import {SessionDTO} from "../Entities/sessionDTO";

export const customAxios = axios.create({
  baseURL: 'https://localhost:5001'
})

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  constructor() { }

  async getSessionsForEmployee(employeeId: number): Promise<Session[]>{
    const response = await customAxios.get('session/user/'+employeeId);
    return response.data;
  }

  async createSession(dto: SessionDTO) {
    const result = await customAxios.post('session', dto);
    return result.data;
  }

  async deleteSession(employeeId: number): Promise<Session>{
    const response = await customAxios.delete('session/'+employeeId);
    return response.data;
  }

}
