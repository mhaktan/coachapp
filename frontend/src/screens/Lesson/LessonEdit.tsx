import React, { useState, useEffect } from 'react';
import { useMutation } from '@tanstack/react-query';
import { TkButton, TkDatepicker, TkInput, TkSelect } from '@takeoff-ui/react';
import { dataProvider } from '../../dataProvider';
import { overlayStyle, modalStyle } from '../../styles';
import { LookupSelect } from '../../shared/LookupSelect';

type LessonRecord = {
  id: string | number;
  lessonDate: string;
  startTime?: string;
  endTime?: string;
  title?: string;
  notes?: string;
  status: string;
  coachId: string;
};

interface LessonEditProps {
  record: LessonRecord | null;
  onClose: () => void;
  onSuccess: () => void;
}

export const LessonEdit: React.FC<LessonEditProps> = ({ record, onClose, onSuccess }) => {
  const [form, setForm] = useState<Partial<LessonRecord>>(record ?? {});
  const setField = (name: string, value: unknown) => setForm((p) => ({ ...p, [name]: value }));

  useEffect(() => {
    if (record) setForm({ ...record });
  }, [record]);

  const mutation = useMutation({
    mutationFn: (values: Partial<LessonRecord>) =>
      dataProvider.update('Lesson', record!.id, values),
    onSuccess: (_data, values) => { onSuccess(); onClose(); },
    onError: (err: Error) => { window.dispatchEvent(new CustomEvent('app-toast', { detail: { type: 'error', message: err.message } })); },
  });

  if (!record) return null;

  return (
    <div style={overlayStyle} onClick={onClose}>
      <div style={modalStyle} onClick={(e) => e.stopPropagation()}>
        <div style={{ padding: '20px 28px 0', flexShrink: 0, display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
          <h2 style={{ margin: 0, fontSize: 18, fontWeight: 700 }}>Edit Lesson</h2>
          <button onClick={onClose} style={{ background: 'none', border: 'none', fontSize: 20, cursor: 'pointer', color: '#666', padding: '4px 8px', borderRadius: 4 }} onMouseOver={(e) => (e.currentTarget.style.color = '#333')} onMouseOut={(e) => (e.currentTarget.style.color = '#666')}>✕</button>
        </div>
        <form onSubmit={(e) => { e.preventDefault(); mutation.mutate(form); }} style={{ display: 'flex', flexDirection: 'column', flex: 1, overflow: 'hidden' }}>
          <div style={{ flex: 1, overflowY: 'auto', padding: '20px 28px' }}>
            <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '16px' }}>
                <div>
                  <TkDatepicker label="Lesson Date *" value={String(form.lessonDate ?? '')} onTkChange={(e: CustomEvent) => ((v) => setField('lessonDate', v))(e.detail)} />
                </div>
                <div>
                  <TkInput mode="text" label="Start Time" value={String(form.startTime ?? '')} onTkChange={(e: CustomEvent) => ((v) => setField('startTime', v))(e.detail)} />
                </div>
                <div>
                  <TkInput mode="text" label="End Time" value={String(form.endTime ?? '')} onTkChange={(e: CustomEvent) => ((v) => setField('endTime', v))(e.detail)} />
                </div>
                <div>
                  <TkInput mode="text" label="Title" value={String(form.title ?? '')} onTkChange={(e: CustomEvent) => ((v) => setField('title', v))(e.detail)} />
                </div>
                <div>
                  <TkInput mode="text" label="Notes" value={String(form.notes ?? '')} onTkChange={(e: CustomEvent) => ((v) => setField('notes', v))(e.detail)} />
                </div>
                <div>
                  <LookupSelect label="Status *" value={String(form.status ?? '')} onChange={(v) => setField('status', v ? Number(v) : null)} searchable={false} options={[{ label: 'Planned', value: '0' }, { label: 'Completed', value: '1' }, { label: 'Cancelled', value: '2' }]} />
                </div>
                <div>
                  <LookupSelect label="Hoca *" resource="Coach" value={String(form.coachId ?? '')} onChange={(v) => setField('coachId', v)} displayField="firstName" />
                </div>
            </div>
          </div>
          <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8, padding: '16px 28px', borderTop: '1px solid #e8e8e8', flexShrink: 0, background: '#fff' }}>
            <TkButton label="Cancel" variant="secondary" onTkClick={onClose} />
            <TkButton label={mutation.isPending ? 'Saving…' : 'Save Changes'} variant="primary" mode="submit" disabled={mutation.isPending} />
          </div>
        </form>
      </div>
    </div>
  );
};
