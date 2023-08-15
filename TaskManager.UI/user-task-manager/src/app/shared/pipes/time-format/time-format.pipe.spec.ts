import { TimeFormatPipe } from './time-format.pipe';

describe('TimeFormatPipe', () => {
  let pipe: TimeFormatPipe;

  beforeEach(() => {
    pipe = new TimeFormatPipe();
  });

  it('should transform "12:34:56" to "12:34"', () => {
    expect(pipe.transform('12:34:56')).toBe('12:34');
  });

  it('should transform undefined to an empty string', () => {
    expect(pipe.transform(undefined)).toBe('');
  });

  it('should transform an empty string to an empty string', () => {
    expect(pipe.transform('')).toBe('');
  });
});
