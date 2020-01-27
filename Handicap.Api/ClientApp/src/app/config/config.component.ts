import { Component, OnInit, Inject } from '@angular/core';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';

import { ConfigService } from './config.service';
import { HandicapConfig } from './handicapConfig';

@Component({
  selector: 'app-config',
  templateUrl: './config.component.html',
  providers: [ConfigService],
  styleUrls: ['./config.component.css']
})
export class ConfigComponent implements OnInit {

  config: HandicapConfig;
  selectedLanguage: string;

  constructor(
    private configService: ConfigService,
    public translate: TranslateService
  ) {
    // translate.addLangs(['de', 'en']);
    translate.use('de');

    this.selectedLanguage = translate.currentLang;
  }

  ngOnInit() {
    this.getConfig();
  }

  getConfig(): void {
    this.configService.getConfig()
      .subscribe(config => {
        this.config = config;
      });
  }

  updateConfig() {
    this.configService.updateConfig(this.config)
      .subscribe(config => {
        this.config = config;
      });
  }

  resetConfig() {
    this.configService.resetConfig()
      .subscribe(config => {
        this.config = config;
      });
  }

  changeLang(lang: string) {
    this.translate.use(lang);
    this.selectedLanguage = lang;
    console.log('using lang: ' + lang);
  }

}
