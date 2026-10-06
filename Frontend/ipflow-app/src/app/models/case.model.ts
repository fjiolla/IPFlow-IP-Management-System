export interface Case {
  caseId: number;
  caseNumber: string;
  title: string;
  description: string;
  status: string;
  openDate: string;
  closeDate?: string;
  caseType: string;
  nextHearingDate?: string;
  clientId: number;
  clientName: string;
  lawyerId?: number;
  lawyerName?: string;
  createdAt: string;
}

export interface CreateCase {
  caseNumber: string;
  title: string;
  description: string;
  status: string;
  openDate: string;
  caseType: string;
  nextHearingDate?: string;
  clientId: number;
  lawyerId?: number;
}

export interface UpdateCase {
  title?: string;
  description?: string;
  status?: string;
  closeDate?: string;
  caseType?: string;
  nextHearingDate?: string;
  lawyerId?: number;
}
