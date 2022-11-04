import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {EmployeesComponent} from "./employees/employees.component";
import {EmployeeCreateComponent} from "./employee-create/employee-create.component";
import {EmployeeSessionsComponent} from "./employee-sessions/employee-sessions.component";
import {SessionCreateComponent} from "./session-create/session-create.component";

const routes: Routes = [
  {path: 'employees', component: EmployeesComponent},
  {path: 'addEmployee', component: EmployeeCreateComponent},
  {path: 'employeeSession/:id', component: EmployeeSessionsComponent},
  {path: 'addSession/:id', component: SessionCreateComponent},
  {path: 'editSession/:id/:ses', component: SessionCreateComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
