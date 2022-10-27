import { Injectable } from '@angular/core';
import axios from 'axios';

export const customAxios = axios.create({
  baseURL: 'https://localhost:5001'
})

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor() { }

  async getSessions(){
    const response = await customAxios.get<any>('session')
    return response.data;
  }

  async insertSession(dto: { startTime: Date; employeeId: number; endTime: Date }) {
    return await customAxios.post('session', dto);
  }
}
