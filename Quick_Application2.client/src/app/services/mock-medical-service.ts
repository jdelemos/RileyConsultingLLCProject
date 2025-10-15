import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { delay } from 'rxjs/operators';
import { MedicalInfo } from '../models/medical-info.model';

@Injectable({ providedIn: 'root' })
export class MockMedicalService {

  private medicalRecords: MedicalInfo[] = [
    {
      inmateId: 'RIM-3024-0881',
      bloodPressure: '120/80',
      heartRate: '72',
      temperature: '98.6',
      respiratoryRate: '16',
      height: "5'11\"",
      weight: '175',
      substanceUse: {
        alcohol: false,
        illegalDrugs: false,
        prescriptionAbuse: false,
        withdrawalRisk: 'No Risk'
      },
      medicalNeeds: 'none',
      housingRestrictions: {
        medicalIsolation: false,
        suicideWatch: false,
        wheelchairAccessible: false
      },
      currentMedications: 'Atenolol 50 mg daily',
      allergies: 'Penicillin',
      chronicConditions: { hypertension: true },
      mentalState: 'Alert and Oriented',
      examinationNotes: 'No acute distress.',
      lastUpdated: new Date()
    }
  ];

  /** GET record by inmate ID */
  getMedicalInfo(inmateId: string): Observable<MedicalInfo | undefined> {
    const record = this.medicalRecords.find(r => r.inmateId === inmateId);
    return of(record).pipe(delay(300));
  }

  /** POST new record */
  createMedicalInfo(info: MedicalInfo): Observable<MedicalInfo> {
    this.medicalRecords.push({ ...info, lastUpdated: new Date() });
    return of(info).pipe(delay(300));
  }

  /** PUT update record */
  updateMedicalInfo(info: MedicalInfo): Observable<MedicalInfo> {
    const idx = this.medicalRecords.findIndex(r => r.inmateId === info.inmateId);
    if (idx !== -1) this.medicalRecords[idx] = { ...info, lastUpdated: new Date() };
    return of(this.medicalRecords[idx]).pipe(delay(300));
  }

  /** DELETE record */
  deleteMedicalInfo(inmateId: string): Observable<boolean> {
    const idx = this.medicalRecords.findIndex(r => r.inmateId === inmateId);
    if (idx !== -1) this.medicalRecords.splice(idx, 1);
    return of(true).pipe(delay(200));
  }

  /** GET all records */
  getAll(): Observable<MedicalInfo[]> {
    return of(this.medicalRecords).pipe(delay(400));
  }
}
