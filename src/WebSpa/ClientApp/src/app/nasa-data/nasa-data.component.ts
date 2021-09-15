import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './nasa-data.component.html',
  styleUrls: ['./nasa-data.component.css']
})
export class NasaDataComponent {
  public imageDates: string[];
  public imagePath: string;
  public httpClient: HttpClient;
  public baseUrl: string;
  public imageLoading: boolean;
  private timeStamp: number;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    this.httpClient = http;
    this.baseUrl = baseUrl;
    this.imageLoading = false;
    this.timeStamp = 0;
  }

  async ngOnInit() {

    await this.getImageDates();
  }

  async getImageDates() {
    this.httpClient.get<string[]>(this.baseUrl + 'v1/marsimage/dates')
      .subscribe(result => {

        this.imageDates = result;
      },
        error => console.error(error));

    //DebugData
    //this.imageDates = ["2018-02-02", "2018-02-02", "2018-02-02", "2018-02-02", "2018-02-02", "2018-02-02", "2018-02-02", "2018-02-02", "2018-02-02", "2018-02-02", "2018-02-02", "2018-02-02", "2018-02-02", "2018-02-02", ]
    //this.imageDates = ["2018-02-02", "2018-02-02", "2018-02-02", "2018-02-02"]
  }

  public async ImageContent(srtingImageDate) {

    await this.getImageContent(srtingImageDate);

  }

  async getImageContent(srtingImageDate) {

    this.imageLoading = true;
    this.httpClient.get<imageContentData>(this.baseUrl + 'v1/marsimage?requestDate=' + srtingImageDate)
      .subscribe(result => {

        this.setLinkPicture(result.imageName);
        this.imageLoading = false;
      },
        error => console.error(error));
  }

  public getLinkPicture() {
    if (this.timeStamp) {
      return this.imagePath + '?' + this.timeStamp;
    }
    return this.imagePath;
  }

  public setLinkPicture(url: string) {
    this.imagePath = url;
    this.timeStamp = (new Date()).getTime();
  }
}

interface imageContentData {
  title: string;
  imageName: string;
  imagePath: string;
  date: string;
}
