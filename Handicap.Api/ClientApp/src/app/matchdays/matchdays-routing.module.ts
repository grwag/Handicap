import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../shared/auth/auth.guard.service';

import { MatchdaysComponent } from './matchdays/matchdays.component';
import { MatchdayDetailComponent } from './matchday-detail/matchday-detail.component';

const routes: Routes = [
    { path: '', component: MatchdaysComponent, canActivate: [AuthGuard] },
    { path: ':id', component: MatchdayDetailComponent, canActivate: [AuthGuard] },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class MatchdaysRoutingModule { }
