import {Injectable, OnInit} from '@angular/core';
import {BehaviorSubject} from 'rxjs';
import {ItemNode} from './node.model';
import { UnitApiService } from '../../api/unit/unit-api.service';
import { ClassificationApiService } from '../../api/unit/classification-api.service';
import { SelectionUnitModel, UnitModel, UnitLightModel } from '../../models/unit.model';
import { Classification } from '../../models/classification.model';
const TREE_DATA = {
    Groceries: {
      'Almond Meal flour': null,
      'Organic eggs': null,
      'Protein Powder': null,
      Fruits: {
        Apple: null,
        Berries: ['Blueberry', 'Raspberry'],
        Orange: null
      }
    },
    Reminders: [
      'Cook dinner',
      'Read the Material Design spec',
      'Upgrade Application to Angular'
    ]
  };
@Injectable()
export class DynamicDataSource{
  dataChange = new BehaviorSubject<ItemNode[]>([]);

  get data(): ItemNode[] { return this.dataChange.value; }

  constructor(private unitService: UnitApiService, private classificationService : ClassificationApiService) {
     this.initialize();

  }
  initialize()
  {
    this.unitService.search("бар", null).subscribe((model : SelectionUnitModel)=> this.handleSeed(model))
  }

  handleSeed(model: SelectionUnitModel)
  {
    console.log(model);
    const data = this.buildFileTree(model.children, 0);
    console.log(data);
    // Notify the change.
    this.dataChange.next(data);
  }

  /**
   * Build the file structure tree. The `value` is the Json object, or a sub-tree of a Json object.
   * The return value is the list of `TodoItemNode`.
   */
  buildFileTree(obj: {[key: string]: any}, level: number): ItemNode[] {

    return Object.keys(obj).slice(0).reduce<ItemNode[]>((accumulator, key, i, arr) => {
      const value = obj[key];
      const node = new ItemNode();
      if(value != null)
      {
        if(Array.isArray(value))
        {
            return this.buildFileTree(value, level);
        }
        if(value.unitType !== undefined)
        {
          node.name = value.name;
          node.id = value.id;
          node.data = value.unitType;
          arr.splice(1, 1);
          node.children = this.buildFileTree(obj[1], level + 1);
        }
        if(value.parentId !== undefined)
        {
          node.name = value.name;
          node.id = value.id;
          node.data = value.parentId;
          node.children = this.buildFileTree(value.children, level + 1);
        }
      }
     

      return accumulator.concat(node);
    }, []);
  }

  /** Add an item to to-do list */
  insertItem(parent: ItemNode, name: string) {
    if (parent.children) {
      parent.children.push({name: name} as ItemNode);
      this.dataChange.next(this.data);
    }
  }

  updateItem(node: ItemNode, name: string) {
    node.name = name;
    this.dataChange.next(this.data);
  }
}