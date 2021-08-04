import moment from 'moment';

const TimeService = {
  extractTimeFromString: (str) => {
    let result = moment(str);
    return result.format('LT');
  },

  calculateTimeLeft: (startTime, duration) => {
    if (startTime === '0001-01-01T00:00:00') return '-';
    else {
      let currentTime = moment();
      let endTime = moment(startTime).add(duration);
      let difference = moment.duration(endTime.diff(currentTime));

      let min = difference.minutes();
      let sec = difference.seconds();
      let sign = '';

      if (min < 0 || sec < 0) sign = '-';

      let minStr = TimeService.AddZero(min);
      let secSrt = TimeService.AddZero(sec);
      return `${sign}${minStr}:${secSrt}`;
    }
  },
  AddZero: (val) => {
    if ((val <= 9 && val >= 0) || (val >= -9 && val < 0))
      return `0${Math.abs(val)}`;
    else return Math.abs(val);
  },
};
export default TimeService;
