export interface Dashboard {
  totalPatents: number;
  activeTrademarks: number;
  pendingCases: number;
  renewalsDue: number;
  totalClients: number;
  upcomingDeadlines: UpcomingDeadline[];
  recentActivities: RecentActivity[];
}

export interface UpcomingDeadline {
  type: string;
  title: string;
  referenceNumber: string;
  date: string;
}

export interface RecentActivity {
  type: string;
  title: string;
  createdAt: string;
}
