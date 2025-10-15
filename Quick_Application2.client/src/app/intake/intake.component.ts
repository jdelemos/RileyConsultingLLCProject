import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-intake',
  standalone: true,
  imports: [],
  templateUrl: './intake.component.html',
  styleUrls: ['./intake.component.scss']
})
export class IntakeComponent {
  // Inmate Data
  inmateName: string = 'Vezey, Colton';
  inmateId: string = '#RIM-3024-0881';
  inmateDob: string = '03/15/1987';
  inmatePhotoUrl: string = '';


  // Progress Tracking
  completedSections: number = 0;
  totalSections: number = 8;
  startTime: string = '5:44';
  lastSaved: string = '';

  // Form Data - Basic Information
  fullLegalName: string = '';
  aliases: string = '';
  dateOfBirth: string = '';
  gender: string = '';
  raceEthnicity: string = '';
  address: string = '';
  nationality: string = '';
  maritalStatus: string = '';
  languagesSpoken: string = '';
  socialSecurityNumber: string = '';
  governmentId: string = '';

  // Form Data - Physical Description
  height: string = '';
  weight: string = '';
  hairColor: string = '';
  eyeColor: string = '';
  scarsMarks: string = '';

  // Form Data - Contact & Emergency
  emergencyContactName: string = '';
  emergencyContactAddress: string = '';
  emergencyContactPhone: string = '';
  emergencyContactRelationship: string = '';

  // Photo Capture Status
  frontMugshotCaptured: boolean = false;
  profileMugshotCaptured: boolean = false;

  // Biometric Status
  fingerprintsCollected: boolean = false;
  irisScanCollected: boolean = false;

  //Router
  constructor(private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    // Initialize component
    this.loadInmateData();
    this.initializeForm();
  }

  loadInmateData(): void {
    // Load inmate data from service
  }

  initializeForm(): void {
    // Initialize form with any existing data
  }

  capturePhoto(type: string): void {
    // Handle photo capture ('front' | 'profile')
  }

  nextPage(): void {
    this.router.navigate(['/medical-intake']);
  }

  saveForm(): void {
    // Save form data
  }

  validateForm(): boolean {
    // Validate required fields
    return true;
  }

  onFormChange(): void {
    // Handle form changes
  }

  calculateProgress(): number {
    return (this.completedSections / this.totalSections) * 100;
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

  collectIrisScan(): void {
    // Handle iris scan collection
  }

  onGenderChange(value: string): void {
    this.gender = value;
    this.onFormChange();
  }

  onRaceEthnicityChange(value: string): void {
    this.raceEthnicity = value;
    this.onFormChange();
  }

  formatSSN(value: string): string {
    // Format SSN as XXX-XX-XXXX
    return value;
  }

  formatPhone(value: string): string {
    // Format phone as XXX-XXX-XXXX
    return value;
  }
}
