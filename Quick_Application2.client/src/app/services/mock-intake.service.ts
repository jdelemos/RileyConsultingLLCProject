import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { delay } from 'rxjs/operators';
import { IntakeInfo } from '../models/intake-info.model';

@Injectable({ providedIn: 'root' })
export class MockIntakeService {
  // Mocked “database”
  private intakeRecords: IntakeInfo[] = [
    {
      inmateId: '',
      fullLegalName: ' ',
      aliases: '',
      dateOfBirth: '',
      gender: '',
      raceEthnicity: '',
      address: '',
      nationality: '',
      maritalStatus: '',
      languagesSpoken: '',
      socialSecurityNumber: '',
      governmentId: 'C',
      height: "",
      weight: '',
      hairColor: '',
      eyeColor: '',
      scarsTattoosMarks: '',
      emergencyContactName: '',
      emergencyContactPhone: '',
      emergencyContactRelationship: '',
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
