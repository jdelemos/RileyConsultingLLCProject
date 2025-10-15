export interface MedicalInfo {
  inmateId: string;

  // Vital Signs
  bloodPressure?: string;
  heartRate?: string;
  temperature?: string;
  respiratoryRate?: string;
  height?: string;
  weight?: string;

  // Substance Use
  substanceUse?: {
    alcohol?: boolean;
    illegalDrugs?: boolean;
    prescriptionAbuse?: boolean;
    withdrawalRisk?: 'No Risk' | 'Low Risk' | 'Moderate Risk' | 'High Risk';
    notes?: string;
  };

  // Medical Clearance
  medicalNeeds?: 'none' | 'routine' | 'urgent';
  housingRestrictions?: {
    medicalIsolation?: boolean;
    suicideWatch?: boolean;
    wheelchairAccessible?: boolean;
  };
  medicalOfficerSignature?: string;

  // Medical History & Exam
  currentMedications?: string;
  allergies?: string;
  chronicConditions?: {
    diabetes?: boolean;
    hypertension?: boolean;
    heartDisease?: boolean;
    asthma?: boolean;
  };
  injuriesOrMarks?: string;
  mentalState?: 'Alert and Oriented' | 'Confused' | 'Agitated' | 'Unresponsive';
  examinationNotes?: string;

  lastUpdated: Date;
}
