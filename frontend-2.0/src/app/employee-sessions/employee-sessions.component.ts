import { Component, OnInit, Input } from '@angular/core';
import {Session} from "../Entities/session";
import {SessionService} from "../services/session.service";
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-employee-sessions',
  templateUrl: './employee-sessions.component.html',
  styleUrls: ['./employee-sessions.component.css']
})
export class EmployeeSessionsComponent implements OnInit {

  sessions: Session[] = [];

  constructor(private sessionService: SessionService,
              private route: ActivatedRoute) { }

  async ngOnInit(){
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.sessions = await this.sessionService.getSessionsForEmployee(id)
  }

  editSession(session: Session) {

  }

  async deleteSession(id: number) {
    const result = await this.sessionService.deleteSession(id);
    this.sessions = this.sessions.filter(s => s.id != result.id);
  }
}
