/**
 * BillingSoftware.Web
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 *//* tslint:disable:no-unused-variable member-ordering */

import { Inject, Injectable, Optional }                      from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent }                           from '@angular/common/http';
import { CustomHttpUrlEncodingCodec }                        from '../encoder';

import { Observable }                                        from 'rxjs';

import { OrderConfirmation } from '../model/orderConfirmation';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';


@Injectable()
export class OrderConfirmationsService {

    protected basePath = '/';
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    constructor(protected httpClient: HttpClient, @Optional()@Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
        if (basePath) {
            this.basePath = basePath;
        }
        if (configuration) {
            this.configuration = configuration;
            this.basePath = basePath || configuration.basePath || this.basePath;
        }
    }

    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    private canConsumeForm(consumes: string[]): boolean {
        const form = 'multipart/form-data';
        for (const consume of consumes) {
            if (form === consume) {
                return true;
            }
        }
        return false;
    }


    /**
     * 
     * 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiOrderConfirmationsGet(observe?: 'body', reportProgress?: boolean): Observable<Array<OrderConfirmation>>;
    public apiOrderConfirmationsGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<OrderConfirmation>>>;
    public apiOrderConfirmationsGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<OrderConfirmation>>>;
    public apiOrderConfirmationsGet(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.accessToken) {
            const accessToken = typeof this.configuration.accessToken === 'function'
                ? this.configuration.accessToken()
                : this.configuration.accessToken;
            headers = headers.set('Authorization', 'Bearer ' + accessToken);
        }
        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<Array<OrderConfirmation>>('get',`${this.basePath}/api/OrderConfirmations`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param orderConfirmationId 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiOrderConfirmationsGetAsPdfOrderConfirmationIdGet(orderConfirmationId: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiOrderConfirmationsGetAsPdfOrderConfirmationIdGet(orderConfirmationId: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiOrderConfirmationsGetAsPdfOrderConfirmationIdGet(orderConfirmationId: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiOrderConfirmationsGetAsPdfOrderConfirmationIdGet(orderConfirmationId: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (orderConfirmationId === null || orderConfirmationId === undefined) {
            throw new Error('Required parameter orderConfirmationId was null or undefined when calling apiOrderConfirmationsGetAsPdfOrderConfirmationIdGet.');
        }

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.accessToken) {
            const accessToken = typeof this.configuration.accessToken === 'function'
                ? this.configuration.accessToken()
                : this.configuration.accessToken;
            headers = headers.set('Authorization', 'Bearer ' + accessToken);
        }
        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<any>('get',`${this.basePath}/api/OrderConfirmations/get-as-pdf/${encodeURIComponent(String(orderConfirmationId))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param orderConfirmationId 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiOrderConfirmationsGetAsWordOrderConfirmationIdGet(orderConfirmationId: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiOrderConfirmationsGetAsWordOrderConfirmationIdGet(orderConfirmationId: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiOrderConfirmationsGetAsWordOrderConfirmationIdGet(orderConfirmationId: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiOrderConfirmationsGetAsWordOrderConfirmationIdGet(orderConfirmationId: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (orderConfirmationId === null || orderConfirmationId === undefined) {
            throw new Error('Required parameter orderConfirmationId was null or undefined when calling apiOrderConfirmationsGetAsWordOrderConfirmationIdGet.');
        }

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.accessToken) {
            const accessToken = typeof this.configuration.accessToken === 'function'
                ? this.configuration.accessToken()
                : this.configuration.accessToken;
            headers = headers.set('Authorization', 'Bearer ' + accessToken);
        }
        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<any>('get',`${this.basePath}/api/OrderConfirmations/get-as-word/${encodeURIComponent(String(orderConfirmationId))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param id 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiOrderConfirmationsIdGet(id: string, observe?: 'body', reportProgress?: boolean): Observable<OrderConfirmation>;
    public apiOrderConfirmationsIdGet(id: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<OrderConfirmation>>;
    public apiOrderConfirmationsIdGet(id: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<OrderConfirmation>>;
    public apiOrderConfirmationsIdGet(id: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling apiOrderConfirmationsIdGet.');
        }

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.accessToken) {
            const accessToken = typeof this.configuration.accessToken === 'function'
                ? this.configuration.accessToken()
                : this.configuration.accessToken;
            headers = headers.set('Authorization', 'Bearer ' + accessToken);
        }
        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<OrderConfirmation>('get',`${this.basePath}/api/OrderConfirmations/${encodeURIComponent(String(id))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param orderConfirmationId 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiOrderConfirmationsOrderConfirmationToDeliveryNoteOrderConfirmationIdPost(orderConfirmationId: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiOrderConfirmationsOrderConfirmationToDeliveryNoteOrderConfirmationIdPost(orderConfirmationId: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiOrderConfirmationsOrderConfirmationToDeliveryNoteOrderConfirmationIdPost(orderConfirmationId: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiOrderConfirmationsOrderConfirmationToDeliveryNoteOrderConfirmationIdPost(orderConfirmationId: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (orderConfirmationId === null || orderConfirmationId === undefined) {
            throw new Error('Required parameter orderConfirmationId was null or undefined when calling apiOrderConfirmationsOrderConfirmationToDeliveryNoteOrderConfirmationIdPost.');
        }

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.accessToken) {
            const accessToken = typeof this.configuration.accessToken === 'function'
                ? this.configuration.accessToken()
                : this.configuration.accessToken;
            headers = headers.set('Authorization', 'Bearer ' + accessToken);
        }
        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<any>('post',`${this.basePath}/api/OrderConfirmations/order-confirmation-to-delivery-note/${encodeURIComponent(String(orderConfirmationId))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param orderConfirmationId 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiOrderConfirmationsOrderConfirmationToInvoiceOrderConfirmationIdPost(orderConfirmationId: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiOrderConfirmationsOrderConfirmationToInvoiceOrderConfirmationIdPost(orderConfirmationId: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiOrderConfirmationsOrderConfirmationToInvoiceOrderConfirmationIdPost(orderConfirmationId: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiOrderConfirmationsOrderConfirmationToInvoiceOrderConfirmationIdPost(orderConfirmationId: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (orderConfirmationId === null || orderConfirmationId === undefined) {
            throw new Error('Required parameter orderConfirmationId was null or undefined when calling apiOrderConfirmationsOrderConfirmationToInvoiceOrderConfirmationIdPost.');
        }

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.accessToken) {
            const accessToken = typeof this.configuration.accessToken === 'function'
                ? this.configuration.accessToken()
                : this.configuration.accessToken;
            headers = headers.set('Authorization', 'Bearer ' + accessToken);
        }
        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<any>('post',`${this.basePath}/api/OrderConfirmations/order-confirmation-to-invoice/${encodeURIComponent(String(orderConfirmationId))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param body 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiOrderConfirmationsPut(body?: OrderConfirmation, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiOrderConfirmationsPut(body?: OrderConfirmation, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiOrderConfirmationsPut(body?: OrderConfirmation, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiOrderConfirmationsPut(body?: OrderConfirmation, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.accessToken) {
            const accessToken = typeof this.configuration.accessToken === 'function'
                ? this.configuration.accessToken()
                : this.configuration.accessToken;
            headers = headers.set('Authorization', 'Bearer ' + accessToken);
        }
        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<any>('put',`${this.basePath}/api/OrderConfirmations`,
            {
                body: body,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
