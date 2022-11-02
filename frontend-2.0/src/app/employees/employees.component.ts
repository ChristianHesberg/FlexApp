import { Component, OnInit } from '@angular/core';
import {EmployeeService} from "../services/employee.service";
import {Employee} from "../Entities/employee";

@Component({
  selector: 'employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {

  employees: Employee[] = [];

  constructor(private employeeService: EmployeeService) { }

  async ngOnInit(){
    this.employees = await this.employeeService.getEmployees();
  }


  sessionDetails(id: number) {

  }
}
