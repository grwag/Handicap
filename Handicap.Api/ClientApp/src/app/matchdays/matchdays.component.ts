import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { MatchdayService } from '../shared/services/matchday.service';
import { MatchdaysDataSource } from '../shared/dataSources/matchdaysDataSource';
import { MatPaginator, MatSort } from '@angular/material';
import { Router, ActivatedRoute } from '@angular/router';
import { merge } from 'rxjs';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-matchdays',
  templateUrl: './matchdays.component.html',
  providers: [MatchdayService],
  styleUrls: ['./matchdays.component.css']
})
export class MatchdaysComponent implements OnInit, AfterViewInit {

  dataSource: MatchdaysDataSource;
  totalMatchdays: number;
  openMatchdays: number;
  displayedColumns: string[] = ['date', 'id'];

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  constructor(private matchdayService: MatchdayService, private router: Router) { }

  ngOnInit() {
    this.setTotalMatchdays();
    this.setOpenMatchdays();
    this.dataSource = new MatchdaysDataSource(this.matchdayService);
    this.dataSource.loadMatchdays('date', true, 10, 0);
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        tap(() => this.loadMatchdays())
      )
      .subscribe();
  }

  setTotalMatchdays() {
    this.matchdayService.getTotalMatchdays()
      .subscribe(res => {
        this.totalMatchdays = res.totalCount;
      });
  }

  setOpenMatchdays() {
    this.matchdayService.getOpenMatchdays()
    .subscribe(res => {
      this.openMatchdays = res.totalCount;
    });
  }

  loadMatchdays() {
    this.dataSource.loadMatchdays(
      this.sort.active,
      (this.sort.direction === 'desc'),
      this.paginator.pageSize,
      this.paginator.pageIndex
    );
  }

  createMatchday() {
    this.matchdayService.createMatchday().subscribe(md => {
      this.loadMatchdays();
      this.setOpenMatchdays();
      this.router.navigate(['/matchdays/' + md.id]);
    });
  }

}
