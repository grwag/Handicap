import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ConfigRoutingModule } from './config-routing.module';
import { ConfigComponent } from './config.component';

import { TranslateModule } from '@ngx-translate/core';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatCardModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
    declarations: [
        ConfigComponent,
    ],
    imports: [
        CommonModule,
        ConfigRoutingModule,
        MatSlideToggleModule,
        MatCardModule,
        TranslateModule,
        FormsModule,
        ReactiveFormsModule,
        MatInputModule,
        MatSelectModule
    ]
})
export class ConfigModule { }
