import { Component, OnInit } from '@angular/core';
import {EmployeeService} from "../services/employee.service";
import {EmployeeDTO} from "../Entities/employeeDTO";

@Component({
  selector: 'app-employee-create',
  templateUrl: './employee-create.component.html',
  styleUrls: ['./employee-create.component.css']
})
export class EmployeeCreateComponent implements OnInit {

  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
  }

  employeeDto: EmployeeDTO={
    name: 'Henrik'
  };

  async create(){
    const result = await this.employeeService.insertEmployee(this.employeeDto);
  }

}
