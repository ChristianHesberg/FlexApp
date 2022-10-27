import {Component, OnInit} from '@angular/core';
import {HttpService} from "../services/http.service";
import {Time} from "@angular/common";
import {MatDatepicker, MatDatepickerInputEvent} from "@angular/material/datepicker";

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

  constructor(private http: HttpService) {
  }

  async ngOnInit(){
    const sessions = await this.http.getSessions();
    console.log(sessions);
  }

  async insertSession(){
    let hoursStart = this.startTime.split(":")
    let hoursEnd = this.endTime.split(":")
    let start = new Date(this.date);
    let end = new Date(this.date);

    start.setHours(Number(hoursStart[0]), Number(hoursStart[1]))
    end.setHours(Number(hoursEnd[0]), Number(hoursEnd[1]))
    let dto = {
      startTime: start,
      endTime: end,
      employeeId: this.employeeId
    };
    const result = await this.http.insertSession(dto);
    console.log(result.data);
  }


  writeSessionName() {
    console.log(this.date);
  }

  addEvent($event: MatDatepickerInputEvent<unknown, unknown | null>) {
    this.date = $event.value;
  }
}
