import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Observable, of } from 'rxjs';

import { MockIntakeService } from '../services/mock-intake.service';
import { IntakeInfo } from '../models/intake-info.model';

@Component({
  selector: 'app-intake',
  imports: [
    CommonModule,
    FormsModule  // ✅ This enables [(ngModel)]
  ],
  templateUrl: './intake.component.html',
  styleUrls: ['./intake.component.scss'],
  
})
export class IntakeComponent implements OnInit {
  inmateId = 'RIM-3024-0881';
  
  showToast = false;
  loading = false;
  intakeData: IntakeInfo = {
    inmateId: this.inmateId,
    fullLegalName: '',
    aliases: '',
    dateOfBirth: '',
    gender: '',
    raceEthnicity: '',
    address: '',
    nationality: '',
    maritalStatus: '',
    languagesSpoken: '',
    socialSecurityNumber: '',
    governmentId: '',
    height: '',
    weight: '',
    hairColor: '',
    eyeColor: '',
    scarsTattoosMarks: '',
    emergencyContactName: '',
    emergencyContactAddress: '',
    emergencyContactPhone: '',
    emergencyContactRelationship: '',
    lastUpdated: new Date()
  };



  //Router
  constructor(private router: Router, private route: ActivatedRoute, private intakeService: MockIntakeService) { }
 

  ngOnInit(): void {
    // Initialize component
    this.loadInmateData();
    this.initializeForm();
    this.loadIntakeData();
  }

  loadIntakeData(): void {
    this.loading = true;

    this.intakeService.getIntakeInfo(this.inmateId).subscribe({
      next: (data: IntakeInfo | undefined) => {
        this.intakeData = data ?? {
          inmateId: this.inmateId,
          fullLegalName: '',
          aliases: '',
          dateOfBirth: '',
          gender: '',
          raceEthnicity: '',
          address: '',
          nationality: '',
          maritalStatus: '',
          languagesSpoken: '',
          socialSecurityNumber: '',
          governmentId: '',
          height: '',
          weight: '',
          hairColor: '',
          eyeColor: '',
          scarsTattoosMarks: '',
          emergencyContactName: '',
          emergencyContactAddress: '',
          emergencyContactPhone: '',
          emergencyContactRelationship: '',
          lastUpdated: new Date()
        };

        this.loading = false;
        console.log('✅ Loaded intake data:', this.intakeData);
      },
      error: () => {
        this.showToast = true;
        this.loading = false;
      }
    });
  }



  loadInmateData(): void {
    // Load inmate data from service
  }

  initializeForm(): void {
    // Initialize form with any existing data
  }
  private intakeInfoStore: IntakeInfo | null = null;

  getIntakeInfo(inmateId: string): Observable<IntakeInfo> {
    if (!this.intakeInfoStore) {
      // Initialize with default structure if none exists yet
      this.intakeInfoStore = {
        inmateId,
        lastUpdated: new Date(),
        fullLegalName: '',
        aliases: '',
        dateOfBirth: '',
        // etc. initialize or leave empty
      } as IntakeInfo;
    }
    return of(this.intakeInfoStore);
  }


  nextPage(): void {
    this.router.navigate(['/medical-intake']);
  }

  saveForm(): void {
    if (!this.intakeData) {
      this.intakeData = { inmateId: this.inmateId, lastUpdated: new Date() } as any;
    }

    // Replace empty strings with null
    for (const key of Object.keys(this.intakeData)) {
      const value = (this.intakeData as any)[key];
      if (value === '' || value === undefined) {
        (this.intakeData as any)[key] = null;
      }
    }

    this.intakeData.lastUpdated = new Date();

    // Log *current* form state before saving
    console.log('🟡 Current form data (before save):', { ...this.intakeData });

    // Mock save to service
    this.intakeService.updateIntakeInfo(this.intakeData).subscribe((updatedData) => {
      console.log('🟢 Form saved successfully:', { ...updatedData });
      alert('Form data successfully saved (mock).');
    });
  }

  getCurrentIntake(): IntakeInfo | null {
    return this.intakeInfoStore;
  }

  saveDraft(): void {
    if (!this.intakeData) {
      console.warn('No intake data found to save.');
      return;
    }

    this.intakeService.updateIntakeInfo(this.intakeData).subscribe({
      next: updated => {
        console.log('Draft saved successfully:', updated);
      },
      error: err => {
        console.error('Error saving draft:', err);
        this.showToast = true;
      }
    });
  }

  validateForm(): boolean {
    // Validate required fields
    return true;
  }

  onFormChange(): void {
    // Handle form changes
  }

  updateIntakeInfo(updated: IntakeInfo): Observable<void> {
    this.intakeInfoStore = { ...this.intakeInfoStore, ...updated, lastUpdated: new Date() };
    return of();
  }

  

  navigateToTab(tabIndex: number): void {
    // Navigate to different tab/section
  }

  uploadPhoto(event: any, type: string): void {
    // Handle photo upload
  }

  collectFingerprints(): void {
    // Handle fingerprint collection
  }

  capturePhoto(type: 'front' | 'profile'): void {
    console.log(`Captured ${type} photo (mock).`);
    // Later this can open a webcam modal or photo upload handler
  }

  openSignaturePad(): void {
    console.log('Opening signature pad (mock).');
    // Placeholder for signature pad logic
  }

  collectIrisScan(): void {
    // Handle iris scan collection
  }


  formatSSN(value: string): string {
    // Format SSN as XXX-XX-XXXX
    return value;
  }

  formatPhone(value: string): string {
    // Format phone as XXX-XXX-XXXX
    return value;
  }

  closeToast(): void {
    this.showToast = false;
  }
}
