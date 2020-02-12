import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule, TranslateLoader, TranslateService } from '@ngx-translate/core';
import { GameTableComponent } from '../../game-table/game-table.component';

import { CreatePlayerComponent } from './create-player.component';
import { MatFormFieldModule, MatProgressSpinnerModule, MatTableModule, MatPaginatorModule, MatInputModule } from '@angular/material';
import { PlayerService } from 'src/app/shared/services/player.service';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('CreatePlayerComponent', () => {
  let component: CreatePlayerComponent;
  let fixture: ComponentFixture<CreatePlayerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CreatePlayerComponent, GameTableComponent],
      imports: [
        FormsModule,
        MatFormFieldModule,
        ReactiveFormsModule,
        MatProgressSpinnerModule,
        MatTableModule,
        MatPaginatorModule,
        TranslateModule,
        RouterTestingModule,
        MatInputModule,
        NoopAnimationsModule,
        TranslateModule.forRoot()
      ],
      providers: [
        PlayerService,
        HttpClient,
        HttpHandler,
        { provide: 'BASE_API_URL', useClass: class { } }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatePlayerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
