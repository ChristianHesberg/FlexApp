import { Injectable } from '@angular/core';
import axios from "axios";
import {Employee} from "../Entities/employee";
import {EmployeeDTO} from "../Entities/employeeDTO";

export const customAxios = axios.create({
  baseURL: 'https://localhost:5001'
})

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor() { }

  async getEmployees(): Promise<Employee[]>{
    const response = await customAxios.get('employee');
    return response.data;
  }

  async insertEmployee(employeeDto: EmployeeDTO) {
    const result = await customAxios.post('employee', employeeDto);
    return result.data;
  }

  async deleteEmployee(id: number) {
    const result = await customAxios.delete('employee/'+id)
    return result.data;
  }

}
