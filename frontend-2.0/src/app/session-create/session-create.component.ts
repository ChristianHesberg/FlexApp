import { Component, OnInit } from '@angular/core';
import {SessionService} from "../services/session.service";
import {SessionDTO} from "../Entities/sessionDTO";
import {MatDatepickerInputEvent} from "@angular/material/datepicker";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-session-create',
  templateUrl: './session-create.component.html',
  styleUrls: ['./session-create.component.css']
})
export class SessionCreateComponent implements OnInit {

  startTime: string = "";
  endTime: string = "";
  date: any;

  constructor(private sessionService: SessionService,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
  }

  async createSession(){
    let start = new Date(this.date);
    let end = new Date(this.date);

    var userTimezoneOffset = start.getTimezoneOffset();
    let hoursStart = this.startTime.split(":")
    let hoursEnd = this.endTime.split(":")

    start.setHours(Number(hoursStart[0]), Number(hoursStart[1]) - userTimezoneOffset)
    end.setHours(Number(hoursEnd[0]), Number(hoursEnd[1]) - userTimezoneOffset)

    let dto: SessionDTO = {
      employeeId: Number(this.route.snapshot.paramMap.get('id')),
      startTime: start,
      endTime: end
    };
    const result = await this.sessionService.createSession(dto);
  }

  addEvent($event: MatDatepickerInputEvent<unknown, unknown | null>) {
    this.date = $event.value;
  }

}
