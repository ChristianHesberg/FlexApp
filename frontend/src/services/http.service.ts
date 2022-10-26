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

  async getProducts(){
    const data = await customAxios.get<any>('session')
  }
}
