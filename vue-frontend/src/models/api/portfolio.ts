export interface Portfolio {
  id: number;
  name: string;
  isGlobal: boolean;
  description?: string;
  tickerIds: number[];
}
