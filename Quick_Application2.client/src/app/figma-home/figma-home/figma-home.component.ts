// src/app/figma-home/figma-home/figma-home.component.ts
import { Component, OnInit, OnDestroy, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subscription, of } from 'rxjs';
import { delay } from 'rxjs/operators';

import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { ConfigurationService } from '../../services/configuration.service';
import { Utilities } from '../../services/utilities';


@Component({
  selector: 'app-figma-home',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './figma-home.component.html',
  styleUrls: ['./figma-home.component.scss']
})
export class FigmaHomeComponent implements OnInit, OnDestroy {

  public alertService = inject(AlertService);
  private configurations = inject(ConfigurationService);

  placeholderItems: Array<{ id: string; title: string; subtitle?: string }> = [];
  isLoading = false;
  loadSub: Subscription | undefined;

  ngOnInit(): void {
    this.loadPlaceholderWidgets();
  }

  ngOnDestroy(): void {
    this.loadSub?.unsubscribe();
  }

  loadPlaceholderWidgets(): void {
    this.isLoading = true;
    this.alertService.startLoadingMessage('', 'Loading preview layout...');

    const sample = [
      { id: 'ph-assignments', title: 'Assignments', subtitle: '14 items' },
      { id: 'ph-workqueue', title: 'Work Queue', subtitle: '30 items' },
      { id: 'ph-activity', title: 'Activity Log', subtitle: 'Recent activity' },
      { id: 'ph-summary', title: 'Quick Summary', subtitle: 'Key metrics' }
    ];

    this.loadSub = of(sample)
      .pipe(delay(220))
      .subscribe({
        next: (data) => {
          this.placeholderItems = data;
          this.alertService.stopLoadingMessage();
          this.alertService.showMessage('Preview', `${data.length} placeholders loaded`, MessageSeverity.info);
          this.isLoading = false;
        },
        error: (err) => {
          this.handleError('Failed to load preview data', err);
        }
      });
  }

  refreshPreview(): void {
    this.loadPlaceholderWidgets();
  }

  clearPreview(): void {
    this.placeholderItems = [];
  }

  private handleError(context: string, error: any): void {
    this.alertService.stopLoadingMessage();

    if (Utilities.checkNoNetwork(error)) {
      this.alertService.showStickyMessage(
        Utilities.noNetworkMessageCaption,
        Utilities.noNetworkMessageDetail,
        MessageSeverity.error, error
      );
      this.offerBackendDevServer();
    } else {
      const errorMessage = Utilities.getHttpResponseMessage(error);
      if (errorMessage) {
        this.alertService.showStickyMessage(context, errorMessage, MessageSeverity.error, error);
      } else {
        this.alertService.showStickyMessage(
          context,
          'An error occurred while processing your request.\nError: ' + Utilities.stringify(error),
          MessageSeverity.error,
          error
        );
      }
    }

    setTimeout(() => { this.isLoading = false; });
  }

  private offerBackendDevServer(): void {
    if (Utilities.checkIsLocalHost(location.origin) && Utilities.checkIsLocalHost(this.configurations.baseUrl)) {
      this.alertService.showDialog(
        'Dear Developer!<br />' +
        'It appears your backend Web API server is inaccessible or not running...<br />' +
        'Would you like to temporarily switch to the fallback development API server below? (Or specify another)',
        DialogType.prompt,
        value => {
          this.configurations.baseUrl = value as string;
          this.alertService.showStickyMessage(
            'API Changed!',
            'The target Web API has been changed to: ' + value,
            MessageSeverity.warn
          );
        },
        null,
        null,
        null,
        this.configurations.fallbackBaseUrl
      );
    }
  }
}
