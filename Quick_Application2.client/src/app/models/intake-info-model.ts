export interface IntakeInfo {
  inmateId: string;

  // Basic Info
  fullLegalName: string;
  aliases?: string;
  dateOfBirth: string;
  gender: string;
  raceEthnicity: string;
  address?: string;
  nationality?: string;
  maritalStatus?: string;
  languagesSpoken?: string;
  socialSecurityNumber?: string;
  governmentId?: string;

  // Physical Description
  height?: string;
  weight?: string;
  hairColor?: string;
  eyeColor?: string;
  scarsTattoosMarks?: string;

  // Emergency Contact
  emergencyContactName: string;
  emergencyContactAddress?: string;
  emergencyContactPhone?: string;
  emergencyContactRelationship?: string;

  lastUpdated: Date;
}
