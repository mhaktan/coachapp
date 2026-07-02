// ---------------------------------------------------------------------------
// Enum definitions — auto-generated from ER model
// Maps integer values to display labels for enum fields
// ---------------------------------------------------------------------------

// Member.status
export const MemberStatusMap: Record<string, string> = {
  '0': 'Active',
  '1': 'Frozen',
  '2': 'Passive'
};
export const MemberStatusOptions = [
  { label: 'Active', value: '0' },
  { label: 'Frozen', value: '1' },
  { label: 'Passive', value: '2' }
];

// Payment.paymentMethod
export const PaymentPaymentMethodMap: Record<string, string> = {
  '0': 'Cash',
  '1': 'CreditCard',
  '2': 'BankTransfer',
  '3': 'Other'
};
export const PaymentPaymentMethodOptions = [
  { label: 'Cash', value: '0' },
  { label: 'CreditCard', value: '1' },
  { label: 'BankTransfer', value: '2' },
  { label: 'Other', value: '3' }
];

// Lesson.status
export const LessonStatusMap: Record<string, string> = {
  '0': 'Planned',
  '1': 'Completed',
  '2': 'Cancelled'
};
export const LessonStatusOptions = [
  { label: 'Planned', value: '0' },
  { label: 'Completed', value: '1' },
  { label: 'Cancelled', value: '2' }
];
