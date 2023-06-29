export interface MenuItem {
    id?: number;
    label?: string;
    icon?: string;
    link?: string;
    subItems?: any;
    parentId?: number;
    isUiElement?: boolean;
    isTitle?: boolean;
    badge?: any;
    isLayout?: boolean;
    permissionId?: number[];

}
