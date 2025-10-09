

import { Component, OnInit, OnDestroy, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { ConfigurationService } from '../../services/configuration.service';
import { Utilities } from '../../services/utilities';
import { InmateService } from '../../services/inmate.service';
import { Inmate } from '../../models/inmate.model';


@Component({
  selector: 'app-inmates',
  templateUrl: './inmates.component.html',
  styleUrl: './inmates.component.scss',
  standalone: true,
  imports: [FormsModule, CommonModule]
})
export class InmatesComponent implements OnInit, OnDestroy {

  public alertService = inject(AlertService); 
  private inmateService = inject(InmateService);
  private configurations = inject(ConfigurationService);

  inmates: Inmate[] = [];
  isLoading = false;
  searchSubscription: Subscription | undefined;

  // Search form fields
  inmateId = '';
  firstName = '';
  lastName = '';
  facility = '';
  status = '';

  ngOnInit() {
    this.loadAllInmates();
  }

  ngOnDestroy() {
    this.searchSubscription?.unsubscribe();
  }

  /** Load all inmates when component initializes */
  loadAllInmates() {
    this.isLoading = true;
    this.alertService.startLoadingMessage('', 'Retrieving inmate records...');

    this.searchSubscription = this.inmateService.getAll().subscribe({
      next: (data) => {
        this.inmates = data;
        this.alertService.stopLoadingMessage();
        this.alertService.showMessage('Inmates', `${data.length} records loaded`, MessageSeverity.success);
        this.isLoading = false;
      },
      error: (error) => {
        this.handleError('Unable to load inmates', error);
      }
    });
  }

  /** Search inmates based on filters */
  searchInmates() {
    this.isLoading = true;
    this.alertService.startLoadingMessage('', 'Searching inmates...');

    const filters = {
      inmateId: this.inmateId,
      firstName: this.firstName,
      lastName: this.lastName,
      facility: this.facility,
      status: this.status
    };

    this.searchSubscription = this.inmateService.search(filters).subscribe({
      next: (data) => {
        this.inmates = data;
        this.alertService.stopLoadingMessage();
        this.alertService.showMessage('Search', `${data.length} result(s) found`, MessageSeverity.success);
        this.isLoading = false;
      },
      error: (error) => {
        this.handleError('Search failed', error);
      }
    });
  }

  /** Handle HTTP/network errors consistently */
  private handleError(context: string, error: any) {
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

  /** Offer QuickApp fallback backend switch when API is unavailable */
  private offerBackendDevServer() {
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

  /** Clears search form fields */
  resetSearch() {
    this.inmateId = '';
    this.firstName = '';
    this.lastName = '';
    this.facility = '';
    this.status = '';
    this.inmates = [];
  }
}
