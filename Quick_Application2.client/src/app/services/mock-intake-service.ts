import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { delay } from 'rxjs/operators';
import { IntakeInfo } from '../models/intake-info-model.model';

@Injectable({ providedIn: 'root' })
export class MockIntakeService {
  // Mocked “database”
  private intakeRecords: IntakeInfo[] = [
    {
      inmateId: 'RIM-3024-0881',
      fullLegalName: 'Vezey, Colton',
      aliases: 'CJ',
      dateOfBirth: '1987-03-15',
      gender: 'Male',
      raceEthnicity: 'Caucasian',
      address: '123 Apple St, Townsville, CA 12345',
      nationality: 'US',
      maritalStatus: 'Single',
      languagesSpoken: 'English',
      socialSecurityNumber: '123-45-6789',
      governmentId: 'CA-DL-991203',
      height: "5'11\"",
      weight: '175',
      hairColor: 'Blonde',
      eyeColor: 'Green',
      scarsTattoosMarks: 'Scar on left forearm',
      emergencyContactName: 'Jane Vezey',
      emergencyContactPhone: '555-123-4567',
      emergencyContactRelationship: 'Mother',
      lastUpdated: new Date()
    }
  ];

  /** GET record by inmate ID */
  getIntakeInfo(inmateId: string): Observable<IntakeInfo | undefined> {
    const record = this.intakeRecords.find(r => r.inmateId === inmateId);
    return of(record).pipe(delay(300));
  }

  /** POST new record */
  createIntakeInfo(info: IntakeInfo): Observable<IntakeInfo> {
    this.intakeRecords.push({ ...info, lastUpdated: new Date() });
    return of(info).pipe(delay(300));
  }

  /** PUT update record */
  updateIntakeInfo(info: IntakeInfo): Observable<IntakeInfo> {
    const idx = this.intakeRecords.findIndex(r => r.inmateId === info.inmateId);
    if (idx !== -1) this.intakeRecords[idx] = { ...info, lastUpdated: new Date() };
    return of(this.intakeRecords[idx]).pipe(delay(300));
  }

  /** DELETE record */
  deleteIntakeInfo(inmateId: string): Observable<boolean> {
    const idx = this.intakeRecords.findIndex(r => r.inmateId === inmateId);
    if (idx !== -1) this.intakeRecords.splice(idx, 1);
    return of(true).pipe(delay(200));
  }

  /** GET all records */
  getAllIntakeRecords(): Observable<IntakeInfo[]> {
    return of(this.intakeRecords).pipe(delay(400));
  }
}
