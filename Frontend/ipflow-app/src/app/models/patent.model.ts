export interface Patent {
  patentId: number;
  applicationNumber: string;
  title: string;
  description: string;
  filingDate: string;
  expiryDate?: string;
  status: string;
  clientId: number;
  clientName: string;
  lawyerId?: number;
  lawyerName?: string;
  createdAt: string;
}

export interface CreatePatent {
  applicationNumber: string;
  title: string;
  description: string;
  filingDate: string;
  expiryDate?: string;
  status: string;
  clientId: number;
  lawyerId?: number;
}

export interface UpdatePatent {
  title?: string;
  description?: string;
  filingDate?: string;
  expiryDate?: string;
  status?: string;
  lawyerId?: number;
}
