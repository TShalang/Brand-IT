import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DesignService } from '../shared/design.service';

@Component({
  selector: 'app-trial',
  templateUrl: './trial.component.html',
  styleUrls: ['./trial.component.css']
})
export class TrialComponent implements OnInit {

  constructor(private route:ActivatedRoute, service: DesignService) { }

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    console.log(id);
  }

}
