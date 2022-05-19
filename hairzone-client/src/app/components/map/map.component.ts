import { Component, OnInit } from '@angular/core';

declare var ol: any;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {

  latitude: number = 51.77;
  longitude: number = 19.46;
  map: any;

  constructor() { }

  ngOnInit(): void {
    this.map = new ol.Map({
      target: 'map',
      layers: [
        new ol.layer.Tile({
          source: new ol.source.OSM()
        })
      ],
      view: new ol.View({
        center: ol.proj.fromLonLat([51.75, 19.45]),
        zoom: 13
      })
    });

    this.setCenter();
    this.addPoint(this.latitude, this.longitude);
  }

  setCenter(): void {
    var view = this.map.getView();
    view.setCenter(ol.proj.fromLonLat([this.longitude, this.latitude]));
    view.setZoom(13);
  }

  addPoint(lat: number, lng: number): void {
    var vectorLayer = new ol.layer.Vector({
      source: new ol.source.Vector({
        features: [new ol.Feature({
          geometry: new ol.geom.Point(ol.proj.transform([lng, lat], 'EPSG:4326', 'EPSG:3857')),
        })]
      }),
      style: new ol.style.Style({
        image: new ol.style.Icon({
          anchor: [0.5, 0.5],
          scale: 1,
          anchorXUnits: "fraction",
          anchorYUnits: "fraction",
          src: "../../assets/map_pin.png",
        })
      })
    });
    this.map.addLayer(vectorLayer);
  }

  clearMap() {
    var mainLayer = this.map.getLayers().getArray()[0];
    var layers = this.map.getLayers().getArray();
    for(var layer of layers)
    {
      if (layer != mainLayer) {
        this.map.removeLayer(layer);
      }
    }
  }

}
