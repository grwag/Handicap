import { Component, OnInit } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { PlayerService } from '../../shared/services/player.service';
import { PlayerRequest } from '../../shared/playerRequest';

@Component({
  selector: 'app-create-player',
  templateUrl: './create-player.component.html',
  styleUrls: ['./create-player.component.css']
})
export class CreatePlayerComponent implements OnInit {

  form = new FormGroup({
    firstName: new FormControl('', [
      Validators.required
    ]),
    lastName: new FormControl('', [
      Validators.required
    ]),
    handicap: new FormControl('', [
      Validators.required,
      this.validateHandicap
    ])
  });
  constructor(
    private playerService: PlayerService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  createPlayer() {
    const playerRequest = new PlayerRequest(
      this.form.controls.firstName.value,
      this.form.controls.lastName.value,
      this.form.controls.handicap.value
    );
    this.playerService.createPlayer(playerRequest)
      .subscribe(player => {
        this.router.navigate(['/players', player.id]);
      });
  }

  validateHandicap(formControl: FormControl) {
    const value = formControl.value;

    if (value % 5 === 0 && (value >= 0 && value <= 100)) {
      return null;
    }

    return {
      handicap: {
        parsedHandicap: value
      }
    };
  }

}
