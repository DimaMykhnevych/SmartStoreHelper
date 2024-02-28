import { Component } from '@angular/core';
import * as RecordRTC from 'recordrtc';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ProductService } from './services/product.service';
import { Product } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  public title = 'smart-helper';

  public recording: boolean = false;
  public url: string | undefined;
  public products: Product[] = [];
  public isLoading: boolean = false;

  private record: any;

  constructor(
    private _domSanitizer: DomSanitizer,
    private _productService: ProductService
  ) {}

  public sanitize(url: string): SafeUrl {
    return this._domSanitizer.bypassSecurityTrustUrl(url);
  }

  public initiateRecording(): void {
    this.recording = true;
    let mediaConstraints = {
      video: false,
      audio: true,
    };
    navigator.mediaDevices
      .getUserMedia(mediaConstraints)
      .then(this.successCallback.bind(this), this.errorCallback.bind(this));
  }

  public successCallback(stream: any): void {
    var options = {
      mimeType: 'audio/wav',
      numberOfAudioChannels: 1,
      sampleRate: 46000,
    };

    // perform multiple recordings without refreshing page
    this.url = undefined;

    var StereoAudioRecorder = RecordRTC.StereoAudioRecorder;
    this.record = new StereoAudioRecorder(stream, options as RecordRTC.Options);
    this.record.record();
  }

  public stopRecording(): void {
    this.recording = false;
    this.record.stop(this.processRecording.bind(this));
  }

  private processRecording(blob: any): void {
    this.url = URL.createObjectURL(blob);

    var fileOfBlob = new File([blob], 'aFileName.wav', {});
    const formData = new FormData();

    formData.append('file', fileOfBlob, 'aFileName.wav');

    this.isLoading = true;
    this._productService.uploadRequest(formData).subscribe((resp) => {
      this.isLoading = false;
      this.products = resp;
    });
  }

  private errorCallback(error: any): void {
    console.log('Can not play audio in your browser', error);
  }
}
