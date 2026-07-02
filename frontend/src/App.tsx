import React, { useState } from 'react';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom';
import { isAuthenticated, clearAuth } from './dataProvider';
import { LoginPage } from './screens/LoginPage';
import { GlobalMenu } from './components/GlobalMenu';
import { CoachList } from './screens/Coach/CoachList';
import { MemberList } from './screens/Member/MemberList';
import { CoachMemberList } from './screens/CoachMember/CoachMemberList';
import { SessionPackageList } from './screens/SessionPackage/SessionPackageList';
import { PaymentList } from './screens/Payment/PaymentList';
import { LessonList } from './screens/Lesson/LessonList';
import { AttendanceList } from './screens/Attendance/AttendanceList';
import { DashboardScreen } from './screens/dashboard/DashboardScreen';
import UserListScreen from './admin/UserListScreen';
import RoleListScreen from './admin/RoleListScreen';

const queryClient = new QueryClient({
  defaultOptions: { queries: { retry: 1, staleTime: 0 } },
});

export const App: React.FC = () => {
  const [authenticated, setAuthenticated] = useState(isAuthenticated());


  const handleLogout = () => {
    clearAuth();
    setAuthenticated(false);
    queryClient.clear();
  };

  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Routes>
          <Route path="*" element={
            authenticated
              ? (
                <GlobalMenu>
                  <Routes>
                    <Route path="/" element={<Navigate to="/dashboard" replace />} />
          <Route path="/dashboard" element={<DashboardScreen />} />
          <Route path="/Coach" element={<CoachList />} />
          <Route path="/Member" element={<MemberList />} />
          <Route path="/CoachMember" element={<CoachMemberList />} />
          <Route path="/SessionPackage" element={<SessionPackageList />} />
          <Route path="/Payment" element={<PaymentList />} />
          <Route path="/Lesson" element={<LessonList />} />
          <Route path="/Attendance" element={<AttendanceList />} />
          <Route path="/users" element={<UserListScreen />} />
          <Route path="/roles" element={<RoleListScreen />} />
                  </Routes>
                </GlobalMenu>
              )
              : <LoginPage onLogin={() => setAuthenticated(true)} />
          } />
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
};
