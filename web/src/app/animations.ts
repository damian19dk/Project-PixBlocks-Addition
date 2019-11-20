import {animate, query, style, transition, trigger} from '@angular/animations';

export const fadeAnimation =
  trigger('routeAnimations', [
    transition('* <=> *', [
      query(
        ':enter',
        [style({
          opacity: 0,
          position: 'absolute'})],
        { optional: true }
      ),
      query(
        ':leave',
        [style({ opacity: 1, position: 'absolute' }), animate('0.25s', style({ opacity: 0 }))],
        { optional: true }
      ),
      query(
        ':enter',
        [style({ opacity: 0, position: 'absolute' }), animate('0.25s', style({ opacity: 1 }))],
        { optional: true }
      )
    ])
  ]);
