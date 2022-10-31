import {Component, OnInit} from '@angular/core';
import {HttpService} from "../services/http.service";
import {MatDatepicker, MatDatepickerInputEvent} from "@angular/material/datepicker";
import {Session} from "./session";


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  startTime: string = "";
  endTime: string = "";
  date: any;
  employeeId: number = 0;
  sessions: Session[] = [];

  constructor(private http: HttpService) {
  }

  async ngOnInit(){
    this.sessions = await this.http.getSessions();
  }

  async insertSession(){
    let start = new Date(this.date);
    let end = new Date(this.date);

    var userTimezoneOffset = start.getTimezoneOffset();
    let hoursStart = this.startTime.split(":")
    let hoursEnd = this.endTime.split(":")

    start.setHours(Number(hoursStart[0]), Number(hoursStart[1]) - userTimezoneOffset)
    end.setHours(Number(hoursEnd[0]), Number(hoursEnd[1]) - userTimezoneOffset)

    let dto = {
      startTime: start,
      endTime: end,
      employeeId: this.employeeId
    };
    const result = await this.http.insertSession(dto);
    this.sessions.push(result);
  }

  addEvent($event: MatDatepickerInputEvent<unknown, unknown | null>) {
    this.date = $event.value;
  }

  async deleteSession(id: number) {
    const session = await this.http.deleteSession(id);
    this.sessions = this.sessions.filter(s => s.id != session.id);
  }
}
